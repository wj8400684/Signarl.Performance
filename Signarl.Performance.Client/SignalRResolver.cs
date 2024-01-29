using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Signarl.Performance.Client;

internal sealed class SignalRResolver : IFormatterResolver
{
    public static readonly IFormatterResolver Instance = new SignalRResolver();

    public static readonly IReadOnlyList<IFormatterResolver> Resolvers = new IFormatterResolver[2]
    {
        DynamicEnumAsStringResolver.Instance,
        ContractlessStandardResolver.Instance
    };

    public IMessagePackFormatter<T> GetFormatter<T>()
    {
        return Cache<T>.Formatter;
    }

    private static class Cache<T>
    {
        public static readonly IMessagePackFormatter<T> Formatter = ResolveFormatter();

        private static IMessagePackFormatter<T> ResolveFormatter()
        {
            foreach (IFormatterResolver resolver in Resolvers)
            {
                IMessagePackFormatter<T> formatter = resolver.GetFormatter<T>();
                if (formatter != null)
                    return formatter;
            }

            return null;
        }
    }
}