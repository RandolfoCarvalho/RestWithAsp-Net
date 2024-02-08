namespace RestWithAspNet.Data.Converter.Contract
{
    public interface IParser<origem, destino>
    {
        destino Parse(origem origem);
        List<destino> Parse(List<origem> origem);


    }
}
