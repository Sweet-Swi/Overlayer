﻿using System;
using Overlayer.Core.JavaScript.Compiler;
using Overlayer.Core.JavaScript.Library;

namespace Overlayer.Core.JavaScript
{
    /// <summary>
    /// Represents the result of compiling a script.
    /// </summary>
    public sealed class CompiledEval
    {
        private GlobalOrEvalMethodGenerator methodGen;

        internal CompiledEval(GlobalOrEvalMethodGenerator methodGen)
        {
            if (methodGen == null)
                throw new ArgumentNullException(nameof(methodGen));
            this.methodGen = methodGen;
        }

        /// <summary>
        /// Compiles source code into a quickly executed form, using the given compiler options.
        /// </summary>
        /// <param name="source"> The javascript source code to execute. </param>
        /// <param name="options"> Compiler options, or <c>null</c> to use the default options. </param>
        /// <returns> A CompiledScript instance, which can be executed as many times as needed. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="source"/> is a <c>null</c> reference. </exception>
        public static CompiledEval Compile(ScriptSource source, CompilerOptions options = null)
        {
            var methodGen = new GlobalOrEvalMethodGenerator(
                source,                             // The source code.
                options ?? new CompilerOptions(),   // The compiler options.
                GlobalOrEvalMethodGenerator.GeneratorContext.GlobalEval);

            // Parse
            methodGen.Parse();

            // Optimize
            methodGen.Optimize();

            // Generate code
            methodGen.GenerateCode();

            return new CompiledEval(methodGen);
        }
        public Type ReturnType { get; private set; }
        /// <summary>
        /// Gets the body of the generated method in the form of disassembled IL code.  Will be
        /// <c>null</c> unless <see cref="CompilerOptions.EnableILAnalysis"/> was set to
        /// <c>true</c>.
        /// </summary>
        public string DisassembledIL
        {
            get { return this.methodGen.GeneratedMethod.DisassembledIL; }
        }

        /// <summary>
        /// Executes the compiled eval code.
        /// </summary>
        /// <param name="engine"> The script engine to use to execute the script. </param>
        /// <returns> The result of the eval. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="engine"/> is a <c>null</c> reference. </exception>
        public object Evaluate(ScriptEngine engine)
        {
            try
            {
                object result = methodGen.Execute(engine, RuntimeScope.CreateGlobalScope(engine), engine.Global);

                // Execute any pending callbacks.
                engine.ExecutePostExecuteSteps();

                return TypeUtilities.NormalizeValue(result);
            }
            finally
            {
                // Ensure the list of post-execute steps is cleared if there is an exception.
                engine.ClearPostExecuteSteps();
            }
        }
        internal GlobalOrEvalMethodGenerator.GlobalCodeDelegate del;
        internal ExecutionContext context;
        internal object EvaluateFastInternal(ScriptEngine engine)
        {
        Run:
            if (context != null)
            {
                object obj = del(context);
                if (obj == null)
                    return null;
                else if (obj is double or uint)
                    return (double)obj;
                else if (obj is ConcatenatedString)
                    obj = ((ConcatenatedString)obj).ToString();
                else if (obj is ClrInstanceWrapper)
                    obj = ((ClrInstanceWrapper)obj).WrappedInstance;
                else if (obj is ClrStaticTypeWrapper)
                    obj = ((ClrStaticTypeWrapper)obj).WrappedType;
                return obj;
            }
            del = (GlobalOrEvalMethodGenerator.GlobalCodeDelegate)methodGen.GeneratedMethod.GeneratedDelegate;
            context = ExecutionContext.CreateGlobalOrEvalContext(engine, RuntimeScope.CreateGlobalScope(engine), engine.Global);
            goto Run;
        }
    }
}
