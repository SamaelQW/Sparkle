using Neo4jClient;
using Sparkle.Neo4j.Domain.Data;
using System;

namespace Sparkle.Neo4j.Domain.Factory
{
    public class GraphClientFactory
    {
        private readonly IGraphSettings _settings;

        public GraphClientFactory(IGraphSettings settings)
        {
            _settings = settings;
        }

        public GraphClient GetClient()
        {
            return new GraphClient(new Uri(_settings.Url), _settings.Login, _settings.Password);
        }
    }
}
