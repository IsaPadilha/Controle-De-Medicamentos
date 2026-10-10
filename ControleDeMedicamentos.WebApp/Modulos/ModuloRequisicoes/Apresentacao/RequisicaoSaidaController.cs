using ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloMedicamento.Dominio;
using ControleDeMedicamentos.WebApp.Modulos.ModuloPacientes.Dominio;
using ControleDeMedicamentos.WebApp.Compartilhado.Apresentacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Apresentacao
{
    public class RequisicaoSaidaController : Controller
    {
        private readonly IRepositorioRequisicaoSaida repositorio;
        private readonly IRepositorioPaciente repositorioPaciente;
        private readonly IRepositorioMedicamento repositorioMedicamento;

        public RequisicaoSaidaController(
            IRepositorioRequisicaoSaida repositorio,
            IRepositorioPaciente repositorioPaciente,
            IRepositorioMedicamento repositorioMedicamento
        )
        {
            this.repositorio = repositorio;
            this.repositorioPaciente = repositorioPaciente;
            this.repositorioMedicamento = repositorioMedicamento;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            var viewModels = new List<ListarRequisicaoSaidaViewModel>();

            foreach (var requisicao in repositorio.SelecionarTodos())
            {
                var medicamentosPrescritosVMs = new List<ListarMedicamentoPrescritoRequisicaoSaidaViewModel>();

                foreach (var prescrito in requisicao.MedicamentoPrescritos)
                {
                    var prescritoVm = new ListarMedicamentoPrescritoRequisicaoSaidaViewModel(
                        prescrito.Medicamento.Id,
                        prescrito.Medicamento.Nome,
                        prescrito.Quantidade
                    );

                    medicamentosPrescritosVMs.Add(prescritoVm);
                }

                var viewModel = new ListarRequisicaoSaidaViewModel(
                    requisicao.Id,
                    requisicao.Paciente.Nome,
                    requisicao.Data,
                    medicamentosPrescritosVMs
                );

                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            var viewModel = new CadastrarRequisicaoSaidaViewModel(Guid.Empty)
            {
                Pacientes = ObterPacientes(),
                MedicamentosPrescritos = ObterMedicamentos()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarRequisicaoSaidaViewModel viewModel)
        {
            var paciente = repositorioPaciente.SelecionarPorId(viewModel.PacienteId);
            if (paciente == null)
                return NotFound();

            var medicamentosPrescritos = new List<MedicamentoPrescrito>();

            foreach (var medicamentoModel in viewModel.MedicamentosPrescritos ?? [])
            {
                if (!medicamentoModel.Selecionado)
                    continue;

                var medicamento = repositorioMedicamento.SelecionarPorId(medicamentoModel.MedicamentoId);
                if (medicamento != null)
                {
                    medicamentosPrescritos.Add(new MedicamentoPrescrito(medicamento, medicamentoModel.Quantidade));
                }
            }

            var requisicao = new ControleDeMedicamentos.WebApp.Modulos.ModuloRequisicoes.Dominio.RequisicaoSaida(paciente, medicamentosPrescritos);

            repositorio.Cadastrar(requisicao);

            return RedirectToAction(nameof(Listar));
        }

        private List<PacienteRequisicaoSaidaViewModel> ObterPacientes()
        {
            var viewModels = new List<PacienteRequisicaoSaidaViewModel>();

            foreach (var paciente in repositorioPaciente.SelecionarTodos())
            {
                var viewModel = new PacienteRequisicaoSaidaViewModel(
                    paciente.Id,
                    paciente.Nome
                );

                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        private List<MedicamentoPrescritoRequisicaoSaidaViewModel> ObterMedicamentos(
            List<MedicamentoPrescritoRequisicaoSaidaViewModel>? valoresEnviados = null
        )
        {
            var valoresPorMedicamento = new Dictionary<Guid, MedicamentoPrescritoRequisicaoSaidaViewModel>();

            if (valoresEnviados != null)
            {
                foreach (var valor in valoresEnviados)
                    valoresPorMedicamento[valor.MedicamentoId] = valor;
            }

            var viewModels = new List<MedicamentoPrescritoRequisicaoSaidaViewModel>();

            foreach (var medicamento in repositorioMedicamento.SelecionarTodos())
            {
                valoresPorMedicamento.TryGetValue(medicamento.Id, out var valor);

                viewModels.Add(new MedicamentoPrescritoRequisicaoSaidaViewModel(
                    medicamento.Id,
                    medicamento.Nome,
                    medicamento.QuantidadeEmEstoque,
                    valor?.Selecionado ?? false,
                    valor?.Quantidade ?? 0
                ));
            }

            return viewModels;
        }
    }
}