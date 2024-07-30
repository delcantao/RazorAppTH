using System;

namespace RazorApp.TH.Model.Apresentacao
{
    public class ParticipacaoSocietariaModel
    {
        public int Qtd_Empresas { get; set; }
        public string Json { get; set; }
        public Dcr_empresas[] Dcr_empresas { get; set; }
    }

    public class Dcr_empresas
    {
        public Dados_Do_CNPJ Dados_Do_CNPJ { get; set; }
        public Dados_Do_Socio Dados_Do_Socio { get; set; }
        public Dados_Do_Representante Dados_Do_Representante { get; set; }
        public string Ind_Origem { get; set; }
        public DateTime Dat_Atz { get; set; }
    }

    public class Dados_Do_CNPJ
    {
        public string Cod_CNPJ { get; set; }
        public string Ind_Matriz { get; set; }
        public string Dcr_Razao_Social { get; set; }
        public string Dcr_Natureza_Juridica { get; set; }
        public string Dat_Abertura { get; set; }
        public string Dcr_CNAE_Principal { get; set; }
        public string Dcr_CNAE_Secundaria { get; set; }
        public string End_Endereco { get; set; }
        public string End_Numero { get; set; }
        public string End_Complemento { get; set; }
        public string End_Bairro { get; set; }
        public string End_Cidade { get; set; }
        public string End_UF { get; set; }
        public string End_CEP { get; set; }
    }

    public class Dados_Do_Socio
    {
        public string Dcr_Nome { get; set; }
        public string Dcr_Qualificacao { get; set; }
        public string Dat_Inclusao { get; set; }
        public string Dcr_Pais { get; set; }
    }

    public class Dados_Do_Representante
    {
        public string Dcr_Nome_Representante { get; set; }
        public string Dcr_Qualificacao_Representante { get; set; }
    }
}