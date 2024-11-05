using Licitacao.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Licitacao.Services
{
    public class Repositorio
    {
        static HttpClient _httpClient;
        public async Task<Modelo> GetLicitacoes(string Palavras, int Paginas, string data_insercao)
        {
            Modelo modelo = new Modelo();

            try
            {
                var uri = "";
                using (_httpClient = new HttpClient())
                {
                    if (Paginas==0)
                    {
                        uri = "https://alertalicitacao.com.br/api/v1/licitacoesAbertas/?data_insercao="+data_insercao+"&palavra_chave=" + Palavras+"&licitacoesPorPagina=100";
                        _httpClient.DefaultRequestHeaders.Add("token", "0613f3ecb85f1084780866e7beb8f1c3");
                    }
                    else
                    {
                        uri = "https://alertalicitacao.com.br/api/v1/licitacoesAbertas/?data_insercao="+data_insercao+"&palavra_chave=" + Palavras+"&licitacoesPorPagina=100&pagina="+Paginas.ToString();
                        _httpClient.DefaultRequestHeaders.Add("token", "0613f3ecb85f1084780866e7beb8f1c3");
                    }

                    var result = _httpClient.GetAsync(uri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        modelo = JsonConvert.DeserializeObject<Modelo>(content);
                        return modelo;
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return null;
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return modelo;
        }

        public async Task<Classificacao> GetClassificacao(string Objeto)
        {
            Classificacao classificacao = new Classificacao();

            try
            {
                var uri = "";
                using (_httpClient = new HttpClient())
                {
                   uri = "https://connectpoint.hellmrf.dev.br/classify?objeto=" + Objeto ;
                   //_httpClient.DefaultRequestHeaders.Add("token", "0613f3ecb85f1084780866e7beb8f1c3");

                    var result = _httpClient.GetAsync(uri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        classificacao = JsonConvert.DeserializeObject<Classificacao>(content);
                        return classificacao;
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return null;
                    }
                    if (result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return classificacao;
        }


    }
}