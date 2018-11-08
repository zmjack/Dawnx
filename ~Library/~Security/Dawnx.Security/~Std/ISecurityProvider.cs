namespace Dawnx.Security
{
    public interface ISecurityProvider
    {
        byte[] Encrypt(byte[] source);
        byte[] Decrypt(byte[] encrypted);
    }
}
