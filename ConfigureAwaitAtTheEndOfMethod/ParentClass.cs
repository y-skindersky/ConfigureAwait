namespace ConfigureAwaitAtTheEndOfMethod
{
    internal class ParentClass
    {
        public ParentClass()
        {
            Inner = new InnerClass(this);
        }
        
        public InnerClass Inner { get; }
    }
}