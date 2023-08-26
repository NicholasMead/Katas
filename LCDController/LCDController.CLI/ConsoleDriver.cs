namespace LCDController.CLI
{
    class ConsoleDriver : IDisplayDriver
    {
        private readonly TextDisplayOptions options = new();

        public ConsoleDriver()
        {}

        public ConsoleDriver(TextDisplayOptions options)
        {
            this.options = options;
        }

        public void Draw(Segment[] segments) => DisplayText(driver => driver.Draw(segments));

        public void Clear() => Console.Clear();

        private void DisplayText(Action<IDisplayDriver> action)
        {
            var textDisplay = new TextDisplayDriver(options);
            
            action(textDisplay);

            Console.WriteLine(textDisplay.Text);
        }
    }
}