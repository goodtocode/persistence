using GoodToCode.Shared.Analytics.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoodToCode.Shared.Analytics.Tests
{
    public static class TextAnalyzerServiceExtensions
    {
            public static IServiceCollection AddTextAnalyzerService(this IServiceCollection collection, IConfiguration config)
            {
                if (collection == null) throw new ArgumentNullException(nameof(collection));
                if (config == null) throw new ArgumentNullException(nameof(config));

                collection.Configure<CognitiveServiceOptions>(config);
                return collection.AddTransient<ITextAnalyzerService, TextAnalyzerService>();
        }
    }
}
