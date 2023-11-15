namespace EntitiesLayer.LinkModels
{
    public class Links
    {
        public string? Href { get; set; }
        public string? Rel { get; set; }
        public string? Method { get; set; }
        public Links()
        {

        }

        public Links(string? href, string? rel, string? method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
