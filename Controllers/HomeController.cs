using Licitacao.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Diagnostics;
using System.Web.WebPages;
using System.Collections.Generic;
using System.Collections;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Bcpg;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Antlr.Runtime.Misc;
using System.Web.UI;
using MySqlX.XDevAPI;

namespace Licitacao.Controllers
{
    [DebuggerDisplay("{DebuggerDisplay,raw}")]
    public class HomeController : Controller
    {
        MySqlConnection conexaoC1 = new MySqlConnection(String.Format(ConfigurationManager.AppSettings["StrConnect"]));
        MySqlConnection conexaoC2 = new MySqlConnection(String.Format(ConfigurationManager.AppSettings["StrConnect"]));

        MySqlConnection conexaoR1 = new MySqlConnection(String.Format(ConfigurationManager.AppSettings["StrRicardo"]));
        MySqlConnection conexaoR2 = new MySqlConnection(String.Format(ConfigurationManager.AppSettings["StrRicardo"]));

        string Palavras = "";

        public ActionResult Index()
        {

            Session["total"] = 0;
            Session["jaimportadas"] = 0;
            Session["novaslicitacoes"] = 0;
            int jaimportadas = 0;
            int novaslicitacoes = 0;

            conexaoR1.Open();

            string SelP = "select pl_id,pl_palavra from tb_palavras_licitacao order by pl_palavra";

            MySqlCommand qrySelectP = new MySqlCommand(SelP, conexaoR1);
            qrySelectP = new MySqlCommand(SelP, conexaoR1);

            MySqlDataReader readerP = qrySelectP.ExecuteReader();

            Palavras="";

            while (readerP.Read())
            {

                string trabalho = readerP["pl_palavra"].ToString();

                conexaoR2.Open();

                string SelP2 = "select pe_excludente from tb_palavras_excludente where pe_palavra=@palavra";
                MySqlCommand qrySelectP2 = new MySqlCommand(SelP2, conexaoR2);
                qrySelectP2 = new MySqlCommand(SelP2, conexaoR2);
                qrySelectP2.Parameters.Add("@palavra", MySqlDbType.Int16).Value = Convert.ToInt16(readerP["pl_id"].ToString());

                MySqlDataReader readerP2 = qrySelectP2.ExecuteReader();

                string excludente="";

                string pal_excludente = "";

                while (readerP2.Read())
                {
                    excludente = readerP2["pe_excludente"].ToString();

                    if (excludente.LastIndexOf(" ")>0)
                        excludente="%22"+excludente+"%22";

                    excludente=excludente.Replace("á", "%C3%A1");
                    excludente=excludente.Replace("à", "%C3%A0");
                    excludente=excludente.Replace("â", "%C3%A2");
                    excludente=excludente.Replace("ã", "%C3%A3");
                    excludente=excludente.Replace("ä", "%C3%A4");
                    excludente=excludente.Replace("Á", "%C3%81");
                    excludente=excludente.Replace("À", "%C3%80");
                    excludente=excludente.Replace("Â", "%C3%82");
                    excludente=excludente.Replace("Ã", "%C3%83");
                    excludente=excludente.Replace("Ä", "%C3%84");
                    excludente=excludente.Replace("è", "%C3%A8");
                    excludente=excludente.Replace("é", "%C3%A9");
                    excludente=excludente.Replace("ê", "%C3%AA");
                    excludente=excludente.Replace("ë", "%C3%AB");
                    excludente=excludente.Replace("È", "%C3%88");
                    excludente=excludente.Replace("É", "%C3%89");
                    excludente=excludente.Replace("Ê", "%C3%8A");
                    excludente=excludente.Replace("Ë", "%C3%8B");
                    excludente=excludente.Replace("ì", "%C3%AC");
                    excludente=excludente.Replace("í", "%C3%AD");
                    excludente=excludente.Replace("î", "%C3%AE");
                    excludente=excludente.Replace("ï", "%C3%AF");
                    excludente=excludente.Replace("Ì", "%C3%8C");
                    excludente=excludente.Replace("Í", "%C3%8D");
                    excludente=excludente.Replace("Î", "%C3%8E");
                    excludente=excludente.Replace("Ï", "%C3%8F");
                    excludente=excludente.Replace("ò", "%C3%B2");
                    excludente=excludente.Replace("ó", "%C3%B3");
                    excludente=excludente.Replace("ô", "%C3%B4");
                    excludente=excludente.Replace("õ", "%C3%B5");
                    excludente=excludente.Replace("ö", "%C3%B6");
                    excludente=excludente.Replace("Ò", "%C3%92");
                    excludente=excludente.Replace("Ó", "%C3%93");
                    excludente=excludente.Replace("Ô", "%C3%94");
                    excludente=excludente.Replace("Õ", "%C3%95");
                    excludente=excludente.Replace("Ö", "%C3%96");
                    excludente=excludente.Replace("ù", "%C3%B9");
                    excludente=excludente.Replace("ú", "%C3%BA");
                    excludente=excludente.Replace("ü", "%C3%BC");
                    excludente=excludente.Replace("Ù", "%C3%99");
                    excludente=excludente.Replace("Ú", "%C3%9A");
                    excludente=excludente.Replace("Ü", "%C3%9C");
                    excludente=excludente.Replace("ç", "%C3%A7");
                    excludente=excludente.Replace("Ç", "%C3%87");
                    excludente=excludente.Replace("ñ", "%C3%B1");
                    excludente=excludente.Replace("Ñ", "%C3%91");

                    pal_excludente=pal_excludente+"+-"+excludente;
                }
                pal_excludente=pal_excludente+",";
                qrySelectP2.Dispose();

                conexaoR2.Close();

                if (trabalho.LastIndexOf(" ")>0)
                    trabalho="%22"+trabalho+"%22";

                trabalho=trabalho.Replace("á", "%C3%A1");
                trabalho=trabalho.Replace("à", "%C3%A0");
                trabalho=trabalho.Replace("â", "%C3%A2");
                trabalho=trabalho.Replace("ã", "%C3%A3");
                trabalho=trabalho.Replace("ä", "%C3%A4");
                trabalho=trabalho.Replace("Á", "%C3%81");
                trabalho=trabalho.Replace("À", "%C3%80");
                trabalho=trabalho.Replace("Â", "%C3%82");
                trabalho=trabalho.Replace("Ã", "%C3%83");
                trabalho=trabalho.Replace("Ä", "%C3%84");
                trabalho=trabalho.Replace("è", "%C3%A8");
                trabalho=trabalho.Replace("é", "%C3%A9");
                trabalho=trabalho.Replace("ê", "%C3%AA");
                trabalho=trabalho.Replace("ë", "%C3%AB");
                trabalho=trabalho.Replace("È", "%C3%88");
                trabalho=trabalho.Replace("É", "%C3%89");
                trabalho=trabalho.Replace("Ê", "%C3%8A");
                trabalho=trabalho.Replace("Ë", "%C3%8B");
                trabalho=trabalho.Replace("ì", "%C3%AC");
                trabalho=trabalho.Replace("í", "%C3%AD");
                trabalho=trabalho.Replace("î", "%C3%AE");
                trabalho=trabalho.Replace("ï", "%C3%AF");
                trabalho=trabalho.Replace("Ì", "%C3%8C");
                trabalho=trabalho.Replace("Í", "%C3%8D");
                trabalho=trabalho.Replace("Î", "%C3%8E");
                trabalho=trabalho.Replace("Ï", "%C3%8F");
                trabalho=trabalho.Replace("ò", "%C3%B2");
                trabalho=trabalho.Replace("ó", "%C3%B3");
                trabalho=trabalho.Replace("ô", "%C3%B4");
                trabalho=trabalho.Replace("õ", "%C3%B5");
                trabalho=trabalho.Replace("ö", "%C3%B6");
                trabalho=trabalho.Replace("Ò", "%C3%92");
                trabalho=trabalho.Replace("Ó", "%C3%93");
                trabalho=trabalho.Replace("Ô", "%C3%94");
                trabalho=trabalho.Replace("Õ", "%C3%95");
                trabalho=trabalho.Replace("Ö", "%C3%96");
                trabalho=trabalho.Replace("ù", "%C3%B9");
                trabalho=trabalho.Replace("ú", "%C3%BA");
                trabalho=trabalho.Replace("ü", "%C3%BC");
                trabalho=trabalho.Replace("Ù", "%C3%99");
                trabalho=trabalho.Replace("Ú", "%C3%9A");
                trabalho=trabalho.Replace("Ü", "%C3%9C");
                trabalho=trabalho.Replace("ç", "%C3%A7");
                trabalho=trabalho.Replace("Ç", "%C3%87");
                trabalho=trabalho.Replace("ñ", "%C3%B1");
                trabalho=trabalho.Replace("Ñ", "%C3%91");

                if (excludente!="")
                    Palavras=Palavras+trabalho+pal_excludente;
                else
                    Palavras=Palavras=Palavras+trabalho+",";
            }

            qrySelectP.Dispose();

            conexaoR1.Close();

            Palavras=Palavras.Substring(0, Palavras.Length-1);
            int Paginas = 0;
            var data_insercao = DateTime.Now.ToString("yyyy-MM-dd");
            Repositorio r = new Repositorio();
            var x = r.GetLicitacoes(Palavras, Paginas, data_insercao);

            int total = Convert.ToInt32(x.Result.TotalLicitacoes);
            Session["total"] = total;

            int paginas = x.Result.Paginas;
            int Licitacaopaginas = x.Result.LicitacoesPorPagina;
            
            /* Henrique - Objeto List para evitar repetição ao gravar na tabela tb_edital */
            List<string> listLicitacao = new List<string>();

            for (int contador = 0; contador <= paginas; contador++)
            {
                var x1 = r.GetLicitacoes(Palavras, contador, data_insercao);
                foreach (var item in x1.Result.Licitacoes)
                {
                    string a = item.Abertura;
                    string objeto = item.Objeto;

                    if (objeto.Substring(0, 1)=="[")
                    {
                        objeto= objeto.Substring(objeto.IndexOf("]")+1, objeto.Length - (objeto.IndexOf("]")+1)).TrimStart();
                    }

                    if (objeto.Substring(0, 3)=="* L")
                    {
                        objeto=objeto.Substring(2, objeto.Length-2);
                        objeto= objeto.Substring(objeto.IndexOf("*")+1, objeto.Length - (objeto.IndexOf("*")+1)).TrimStart();
                    }

                    string[] palavras = objeto.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '"' }, StringSplitOptions.RemoveEmptyEntries);

                    conexaoC2.Open();

                    string SelM = "select ID from tb_modalidade where CodigoEdital=@modalidade";
                    MySqlCommand qrySelectM = new MySqlCommand(SelM, conexaoC2);
                    qrySelectM = new MySqlCommand(SelM, conexaoC2);
                    qrySelectM.Parameters.Add("@modalidade", MySqlDbType.Int32).Value = Convert.ToInt32(item.Id_tipo);
                    MySqlDataReader readerM = qrySelectM.ExecuteReader();
                    string modalidade = "";

                    while (readerM.Read())
                    {
                        modalidade = readerM["ID"].ToString();
                    }

                    // Teste_ 2
                    qrySelectM.Dispose();

                    conexaoC2.Close();

                    string uf = "";

                    conexaoC2.Open();

                    string SelEs = "select ID from tp_estados where Sigla=@estado";
                    MySqlCommand qrySelectEs = new MySqlCommand(SelEs, conexaoC2);
                    qrySelectEs = new MySqlCommand(SelEs, conexaoC2);
                    qrySelectEs.Parameters.Add("@estado", MySqlDbType.VarChar, 3).Value = item.Uf;
                    MySqlDataReader readerEs = qrySelectEs.ExecuteReader();

                    while (readerEs.Read())
                    {
                        uf = readerEs["ID"].ToString();
                    }

                    qrySelectM.Dispose();

                    conexaoC2.Close();


                    string municipio = "";

                    conexaoC2.Open();

                    string SelMu = "select ID from tp_cidades where Codigo=@municipio";
                    MySqlCommand qrySelectMu = new MySqlCommand(SelMu, conexaoC2);
                    qrySelectMu = new MySqlCommand(SelMu, conexaoC2);
                    qrySelectMu.Parameters.Add("@municipio", MySqlDbType.VarChar, 3).Value = item.Municipio_IBGE;
                    MySqlDataReader readerMu = qrySelectMu.ExecuteReader();

                    while (readerMu.Read())
                    {
                        municipio = readerMu["ID"].ToString();
                    }

                    qrySelectM.Dispose();

                    conexaoC2.Close();

                    /* Henrique - Se UF e Municipio em braco loop/continue */
                    string idLicitacao = item.Id_licitacao;
                    if (uf.IsEmpty() && municipio.IsEmpty())
                     continue;

                    string cStringArray = idLicitacao + uf + municipio;
                    string[] ifFound = listLicitacao.ToArray();
                    bool exists = false;
                    if (ifFound.Contains(cStringArray))
                    {
                        exists = true;
                    }

                    if (exists)
                        continue;

                    conexaoR2.Open();

                    /* Verifico se ja existe a licitação em db_edital.tb_edital */
                    string idEdital = "";
                    string SelEdit = "select ID from tb_edital where CodigoLicitacao=@idLicitacao and ID_cidade=@municipio and ID_Estado=@estado";
                    MySqlCommand qrySelectEdit = new MySqlCommand(SelEdit, conexaoR2);
                    qrySelectEdit = new MySqlCommand(SelEdit, conexaoR2);
                    qrySelectEdit.Parameters.Add("@idLicitacao", MySqlDbType.VarChar, 255).Value = item.Id_licitacao;
                    qrySelectEdit.Parameters.Add("@municipio", MySqlDbType.Int32).Value = Convert.ToInt32(municipio);
                    qrySelectEdit.Parameters.Add("@estado", MySqlDbType.Int32).Value = Convert.ToInt32(uf);
                    MySqlDataReader readerEdit = qrySelectEdit.ExecuteReader();

                    while (readerEdit.Read())
                    {
                        idEdital = readerEdit["ID"].ToString();
                    }

                    qrySelectEdit.Dispose();

                    conexaoR2.Close();

                    if (!idEdital.IsNullOrWhiteSpace())
                    {
                        jaimportadas++;
                        continue;
                    }
                        


                    listLicitacao.Add(cStringArray);
                    /* Fim Se UF e Municipio em braco loop/continue */

                    conexaoR2.Open();

                    string Ins = "insert into tb_edital(ID_Cidade,ID_Estado,CaminhoAnexo,CodigoLicitacao,DataAbertura,DataPublicacao,EmailContato,FonteLicitacao,Modalidade,Nome,Objeto,Observacoes,OrgaoResponsavel,TelefoneContato,TipoLocalizacao,ValorLicitacao,ValorSigiloso,DataCadastro,DataEdicao,DataExclusao) values(@Cidade,@Estado,@Anexo,@Codigo,@Abertura,@Publicacao,@Email,@Licitacao,@Modalidade,@Nome,@Objeto,@Obs,@Responsavel,@Contato,@Localizacao,@Licitado,@Sigiloso,@Cadastro,@Edicao,@Exclusao)";
                    MySqlCommand qryInsert = new MySqlCommand(Ins, conexaoR2);
                    qryInsert = new MySqlCommand(Ins, conexaoR2);
                    if (municipio.Length>0)
                        qryInsert.Parameters.Add("@Cidade", MySqlDbType.Int32).Value = Convert.ToInt32(municipio);
                    else
                        qryInsert.Parameters.Add("@Cidade", MySqlDbType.Int32).Value = null;
                    if (uf.Length>0)
                        qryInsert.Parameters.Add("@Estado", MySqlDbType.Int32).Value = Convert.ToInt32(uf);
                    else
                        qryInsert.Parameters.Add("@Estado", MySqlDbType.Int32).Value =null;
                    qryInsert.Parameters.Add("@Anexo", MySqlDbType.VarChar, 2083).Value = "";
                    qryInsert.Parameters.Add("@Codigo", MySqlDbType.VarChar, 255).Value = item.Id_licitacao;
                    qryInsert.Parameters.Add("@Abertura", MySqlDbType.DateTime).Value = Convert.ToDateTime(item.Abertura_datetime);
                    qryInsert.Parameters.Add("@Publicacao", MySqlDbType.DateTime).Value = DateTime.Now;
                    qryInsert.Parameters.Add("@Email", MySqlDbType.VarChar, 255).Value = null;
                    qryInsert.Parameters.Add("@Licitacao", MySqlDbType.VarChar, 2083).Value = item.LinkExterno;
                    qryInsert.Parameters.Add("@Modalidade", MySqlDbType.Int32).Value = Convert.ToInt32(modalidade);
                    qryInsert.Parameters.Add("@Nome", MySqlDbType.VarChar, 255).Value = item.Titulo;
                    qryInsert.Parameters.Add("@Objeto", MySqlDbType.Text).Value = objeto;
                    qryInsert.Parameters.Add("@Obs", MySqlDbType.Text).Value = null;
                    qryInsert.Parameters.Add("@Responsavel", MySqlDbType.VarChar, 255).Value = item.Orgao;
                    qryInsert.Parameters.Add("@Contato", MySqlDbType.VarChar, 20).Value = null;
                    qryInsert.Parameters.Add("@Localizacao", MySqlDbType.Int64).Value = 1;
                    if (item.Valor!=null)
                        qryInsert.Parameters.Add("@Licitado", MySqlDbType.Decimal).Value = Convert.ToDecimal(item.Valor);
                    else
                        qryInsert.Parameters.Add("@Licitado", MySqlDbType.Decimal).Value = 0;

                    qryInsert.Parameters.Add("@Sigiloso", MySqlDbType.Bit, 1).Value = 2;
                    qryInsert.Parameters.Add("@Cadastro", MySqlDbType.DateTime).Value = DateTime.Now;
                    qryInsert.Parameters.Add("@Edicao", MySqlDbType.DateTime).Value = null;
                    qryInsert.Parameters.Add("@Exclusao", MySqlDbType.DateTime).Value = null;

                    try
                    {
                        qryInsert.ExecuteNonQuery();
                        novaslicitacoes++;
                    }
                    catch
                    {
                        Console.Write("Insert no Banco da Connectpoint.");
                    }
                    finally
                    {
                        qryInsert.Dispose();

                        conexaoR2.Close();
                    }
                }
            }

            Session["novaslicitacoes"] = novaslicitacoes;
            Session["jaimportadas"] = jaimportadas;

            return View();
        }
    }
}