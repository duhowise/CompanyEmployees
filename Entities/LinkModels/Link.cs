using Entities.Models;

namespace Entities.LinkModels
{
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public Link()
        { }

        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
    public class LinkResourceBase
    {
        public LinkResourceBase()
        { }

        public List<Link> Links { get; set; } = new List<Link>();
    }
    public class LinkResponse
    {
        public bool HasLinks { get; set; }

        public List<Entity> ShapedEntities { get; set; }

        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<Entity>();
            ShapedEntities = new List<Entity>();
        }
    }

    public class LinkCollectionWrapper<T> : LinkResourceBase
    {
        public List<T> Value { get; set; } = new List<T>();

        public LinkCollectionWrapper()
        { }

        public LinkCollectionWrapper(List<T> value)
        {
            Value = value;
        }
    }
}