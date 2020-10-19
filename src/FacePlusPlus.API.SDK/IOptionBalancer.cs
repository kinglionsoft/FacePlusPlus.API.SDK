namespace FacePlusPlus.API.SDK
{
    public interface IOptionBalancer<out T>
    {
        T Next();

        int Count { get; }
    }
}