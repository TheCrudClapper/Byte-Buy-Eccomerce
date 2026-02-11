namespace ByteBuy.Core.ServiceContracts;

public interface IPdfGenerator<TModel>
{
    byte[] Generate(TModel model);
}
