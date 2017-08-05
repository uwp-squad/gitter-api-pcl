using Bayeux;

namespace GitterSharp.Realtime
{
    internal class TokenBayeuxProtocolExtension : BayeuxProtocolExtension
    {
        public string Token { get; set; }

        public TokenBayeuxProtocolExtension() : this("token")
        {
        }
        public TokenBayeuxProtocolExtension(string name) : base(name)
        {
        }

        public override bool TryExtendOutgoing(IBayeuxMessage message, out object extension)
        {
            extension = Token;
            return true;
        }
    }
}
