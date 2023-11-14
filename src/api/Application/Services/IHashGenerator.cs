namespace Application.Services;

public interface IHashGenerator
{
    string GenerateHash(string clearText);
}
