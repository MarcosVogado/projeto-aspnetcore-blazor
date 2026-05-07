using ScreenSound.Shared.Modelos.Response;
using System.Net.Http.Json;

namespace ScreenSound.WebAssembly.Services;

public class MusicasAPI
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MusicasAPI> _logger;
 
    public MusicasAPI(IHttpClientFactory factory, ILogger<MusicasAPI> logger)
    {
        _httpClient = factory.CreateClient("API");
        _logger = logger;
    }

    public async Task<ICollection<MusicaResponse>> GetMusicasAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando a requisição para obter as músicas.");
            var resultado = await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("musicas");
            _logger.LogInformation($"Requisição concluída. Foram obtidas {resultado?.Count ?? 0} músicas.");
            return resultado;
        }
        catch (Exception ex)
        {
            string mensagemErro = $"Ocorreu um erro ao obter as músicas.";
            _logger.LogError(ex, mensagemErro);
            throw new ApplicationException(mensagemErro, ex);
        }
    }
}
