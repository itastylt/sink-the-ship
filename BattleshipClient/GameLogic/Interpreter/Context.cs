
public class Context
{
        
        private string Input;

        private string Output;
        
        public Context(string input, string output)
        {
            this.Input = input;
            this.Output = output;
        }

        public string GetInput() { return Input; } 
        public string GetOutput() { return Output; }

        public void SetInput(string input) { this.Input = input; }

        public void SetOutput(string output) { this.Output = output; }
}
