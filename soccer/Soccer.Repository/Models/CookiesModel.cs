namespace Soccer.Repository.Models
{
    public class CookiesModel
    {
        public string Website { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }
        public string Domain { get; set; }

        public CookiesModel() { }

        public CookiesModel(string website, string name, string value, string path, string domain)
        {
            Website = website;
            Name = name;
            Value = value;
            Path = path;
            Domain = domain;
        }
    }
}
