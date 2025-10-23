using Infrastruture.Worker.DTO;

namespace Infrastruture.Worker.Interfaces
{
    public interface ISendEmailService
    {
        public Task<bool> SendEmailAsync(EmailDTO dto);
    }
}
