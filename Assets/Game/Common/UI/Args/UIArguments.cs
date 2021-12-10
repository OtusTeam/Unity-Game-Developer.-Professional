using System;

namespace Prototype
{
    public readonly struct UIArguments
    {
        private readonly UIArgument[] args;

        public UIArguments(params UIArgument[] args)
        {
            this.args = args;
        }

        public T Get<T>(UIArgumentName name)
        {
            if (this.args == null)
            {
                throw new Exception($"Arg {name} is not found");
            }
            
            for (int i = 0, count = this.args.Length; i < count; i++)
            {
                var arg = this.args[i];
                if (arg.Name == name)
                {
                    return (T) arg.Value;
                }
            }
            
            throw new Exception($"Arg {name} is not found");
        }
    }
}