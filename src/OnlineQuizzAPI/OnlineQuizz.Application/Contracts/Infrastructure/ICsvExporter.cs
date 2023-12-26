using OnlineQuizz.Application.Features.Events.Queries.GetEventsExport;
using System.Collections.Generic;

namespace OnlineQuizz.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
