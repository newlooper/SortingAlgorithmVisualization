namespace Performance
{
    public static partial class PerformanceQueue
    {
        public partial class Step
        {
            public static Step CreateStepForCodeLine( string key )
            {
                var step = new Step
                {
                    CodeLineKey = key,
                    PerformanceEffect = PerformanceEffect.CodeLine
                };
                return step;
            }
        }
    }
}