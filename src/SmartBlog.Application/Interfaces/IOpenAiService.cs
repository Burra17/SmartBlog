namespace SmartBlog.Application.Interfaces;

// Interface (kontrakt) för OpenAI-tjänsten - definierar vad vi kan göra med AI:n
// Ligger i Application-lagret så att handlern kan använda det utan att bero på Infrastructure
// Implementationen (OpenAiService) ligger i Infrastructure-lagret
public interface IOpenAiService
{
    // Skicka in textinnehåll och få tillbaka en AI-genererad sammanfattning
    Task<string> GenerateSummaryAsync(string content, CancellationToken cancellationToken = default);
}
