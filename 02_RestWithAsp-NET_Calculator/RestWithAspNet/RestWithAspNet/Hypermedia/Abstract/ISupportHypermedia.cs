namespace RestWithAspNet.Hypermedia.Abstract
{
    public interface ISupportHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
