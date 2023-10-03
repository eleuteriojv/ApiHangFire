using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace ApiHangFire.Controllers
{
    public class ProdutosController : Controller
    {
        [HttpGet]
        [Route("login")]
        public String Login()
        {
            //Fire-and-Forget - job executado somente uma vez
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Bem-Vindo a loja virtual!"));
            return $"Job ID: {jobId}. Email de boas vindas envidado ao cliente!";
        }

        [HttpGet]
        [Route("produtocheckout")]
        public String CheckoutProduto()
        {
            // Delayed Job - este job é executado somente uma vez mas não
            // imediatamente após algum tempo
            var jobId = BackgroundJob.Schedule(() =>
           Console.WriteLine("Seu produto foi incluído no checkout !"), TimeSpan.FromSeconds(20));

            return $"Job ID: {jobId}. Produto adicionado ao seu checkout com sucesso!";
        }

        [HttpGet]
        [Route("produtopagamento")]
        public String ProdutoPagamento()
        {
            //Fire and Forget Job - este job é executado apenas uma vez
            var parentjobId = BackgroundJob.Enqueue(() =>
                  Console.WriteLine("Seu pagamento foi realizado com sucesso!"));

            //Continuations Job - Este job é executado quando o job pai é executado
            BackgroundJob.ContinueJobWith(parentjobId, () =>
                   Console.WriteLine("Pagamento do produto enviado!"));

            return "Você concluiu o pagamento e o recibo foi enviado para o seu email id!";
        }

        [HttpGet]
        [Route("ofertasdiarias")]
        public String OfertasDiarias()
        {
            //Recurring Job - este job é executado muitas vezes em um cronograma especificado
            RecurringJob.AddOrUpdate(() =>
                Console.WriteLine("Envio de produtos similares e sugestões de compra"), Cron.Daily);

            return "## Oferta enviada! ##";
        }
    }
}
