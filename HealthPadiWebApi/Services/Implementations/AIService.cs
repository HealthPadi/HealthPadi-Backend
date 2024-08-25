using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Azure.AI.OpenAI;
using System.Collections.Generic;
using Azure;
using OpenAI.Chat;
using OpenAI.Assistants;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;



namespace HealthPadiWebApi.Services.Implementations
{
    public class AIService : IAIService
    {
        private readonly IConfiguration _configuration;
        private readonly string _endpoint;
        private readonly string _apiKey;
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;

        public AIService(IConfiguration configuration)
        {
            _configuration = configuration;
            _endpoint = _configuration["AzureConfig:OpenAI:OpenAIUrl"];
            _apiKey = _configuration["AzureConfig:OpenAI:OpenAIKey"];
            _azureClient = new AzureOpenAIClient(new Uri(_endpoint), new AzureKeyCredential(_apiKey));
            _chatClient = _azureClient.GetChatClient(_configuration["AzureConfig:OpenAI:ChatEngine"]);
        }

        public async Task<string> GenerateHealthFeeds()
        {
            try
            {
                var topic = HealthyLivingTopics.GetRandomTopic();

                if (topic == "No more topics available")
                {
                    throw new InvalidOperationException("No more topics available");
                }

                var systemMessage = new Models.ChatMessage
                {
                    Role = "system",
                    Content = "You are a helpful AI health assistant. Your task is to generate an article on the given topic" +
                  " Each generated content should be between 100 and 300 words." +
                  " Do not prescribe drugs in any of your articles." +
                  " Let the first line of your article be the topic in capital letters."
                };

                var userMessage = new Models.ChatMessage()
                {
                    Role = "user",
                    Content = $"Please, generate an article on ${topic}"
                };

                var promptConstruct = new List<Models.ChatMessage> { systemMessage, userMessage };
                var openAiPrompt = promptConstruct.Select(msg => MapToOpenAIChatMessage(msg)).ToList();

                ChatCompletion aiResponse = await _chatClient.CompleteChatAsync(openAiPrompt, new ChatCompletionOptions() { Temperature = 0.7f, MaxTokens = 500 });
                if (aiResponse == null)
                {
                    throw new InvalidOperationException("Error getting response from Bot");
                }

                var fullText = string.Join(" ", aiResponse.Content.Select(part => part.ToString()));

                return fullText;

            }
            catch (Exception e)
            {
                   throw new InvalidOperationException("Error generating health feeds", e);
            }
           
        }

        public string GenerateReportSummary(string prompt)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<string> ChatWithAI(ChatRequest request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_endpoint) || string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("Endpoint or API key is not configured properly.");
            }

            var history = request.ChatHistory;

            if (!history.Any(msg => msg.Role == "system"))
            {
                var systemMessage = new Models.ChatMessage
                {
                    Role = "system",
                    Content = $"You are an AI assistant that helps people with health information. You SHOULD NOT give any prescription." +
                    $" For anything other than health questions, respond with 'I am a helpful health assistant, I can only answer health questions.' "
                };

                history.Insert(0, systemMessage);
            }

            history.Add(new Models.ChatMessage { Role = "user", Content = request.NewMessage });

            var openAiHistory = history.Select(msg => MapToOpenAIChatMessage(msg)).ToList();

            await foreach (var stream in _chatClient.CompleteChatStreamingAsync(openAiHistory, new ChatCompletionOptions() { Temperature = 0.7f, MaxTokens = 500 }, cancellationToken))
            {
                foreach (ChatMessageContentPart chunk in stream.ContentUpdate)
                {
                    yield return chunk.Text;
                }
            }
        }

        private OpenAI.Chat.ChatMessage MapToOpenAIChatMessage(Models.ChatMessage msg)
        {
            return msg.Role switch
            {
                "system" => new SystemChatMessage(msg.Content),
                "user" => new UserChatMessage(msg.Content),
                "assistant" => new AssistantChatMessage(msg.Content),
                _ => throw new InvalidOperationException($"Unknown role: {msg.Role}")
            };
        }
    }
}
    