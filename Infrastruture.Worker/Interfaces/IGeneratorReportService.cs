using Infrastruture.Worker.DTO;

namespace Infrastruture.Worker.Interfaces
{
    public interface IGeneratorReportService
    {
        public Task<byte[]> GenerateReservaPdfAsync(EmailDTO dto);
    }
}
