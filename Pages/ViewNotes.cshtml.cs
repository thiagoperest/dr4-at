using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace dr4_at.Pages
{
    [Authorize]
    public class ViewNotesModel : PageModel
    {
        private readonly string _pathArquivos;

        public ViewNotesModel(IWebHostEnvironment env)
        {
            _pathArquivos = System.IO.Path.Combine(env.WebRootPath, "files");
        }

        [BindProperty]
        [Required(ErrorMessage = "Nome do arquivo é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 50 caracteres")]
        public string NomeArquivo { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Conteúdo da nota é obrigatório")]
        [MinLength(1, ErrorMessage = "Conteúdo não pode estar vazio")]
        public string ConteudoNota { get; set; } = string.Empty;

        public string? MensagemSucesso { get; set; }
        public List<string> ArquivosDisponiveis { get; set; } = new();
        public string? ConteudoVisualizacao { get; set; }
        public string? ArquivoVisualizando { get; set; }

        public void OnGet()
        {
            CarregarArquivosDisponiveis();
        }

        public IActionResult OnPost(string? action, string? arquivoSelecionado)
        {
            if (action == "criar")
            {
                if (!ModelState.IsValid)
                {
                    CarregarArquivosDisponiveis();
                    return Page();
                }

                try
                {
                    if (!System.IO.Directory.Exists(_pathArquivos))
                    {
                        System.IO.Directory.CreateDirectory(_pathArquivos);
                    }

                    var nomeCompleto = NomeArquivo.EndsWith(".txt") ? NomeArquivo : $"{NomeArquivo}.txt";
                    var path = System.IO.Path.Combine(_pathArquivos, nomeCompleto);

                    System.IO.File.WriteAllText(path, ConteudoNota);

                    MensagemSucesso = $"Nota '{nomeCompleto}' salva com sucesso!";
                    NomeArquivo = string.Empty;
                    ConteudoNota = string.Empty;
                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    MensagemSucesso = $"Erro ao salvar arquivo: {ex.Message}";
                }
            }
            else if (action == "visualizar" && !string.IsNullOrEmpty(arquivoSelecionado))
            {
                ModelState.Clear();

                try
                {
                    var path = System.IO.Path.Combine(_pathArquivos, arquivoSelecionado);

                    if (System.IO.File.Exists(path))
                    {
                        ConteudoVisualizacao = System.IO.File.ReadAllText(path);
                        ArquivoVisualizando = arquivoSelecionado;
                    }
                }
                catch (Exception ex)
                {
                    MensagemSucesso = $"Erro ao ler arquivo: {ex.Message}";
                }
            }

            CarregarArquivosDisponiveis();
            return Page();
        }

        private void CarregarArquivosDisponiveis()
        {
            try
            {
                if (System.IO.Directory.Exists(_pathArquivos))
                {
                    var arquivos = System.IO.Directory.GetFiles(_pathArquivos, "*.txt");
                    ArquivosDisponiveis = arquivos.Select(System.IO.Path.GetFileName).ToList();
                }
            }
            catch
            {
                ArquivosDisponiveis = new List<string>();
            }
        }
    }
}