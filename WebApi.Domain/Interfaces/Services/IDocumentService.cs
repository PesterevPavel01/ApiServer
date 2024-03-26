using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Order;
using WebApi.Domain.Dto.Report;
using WebApi.Domain.Result;

namespace WebApi.Domain.Interfaces.Services
{
    public interface IDocumentService
    {
        /// <summary>
        /// Получение всех документов за период
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        Task<CollectionResult<DocumentDto>> GetDocumentsAsync(DateTime dateStart, DateTime dateEnd);

        /// <summary>
        /// Получение документов определенной статьи за период
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="expenditureId"></param>
        /// <returns></returns>
        Task<CollectionResult<DocumentDto>> GetDocumentsByExpenditureAsync(DateTime dateStart, DateTime dateEnd, string expenditureName);
        
        /// <summary>
        /// Получение документо по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResult<DocumentDto>> GetDocumentByIdAsync(long id);

        /// <summary>
        /// Создание документа с базовыми параметрами
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<DocumentDto>> CreateDocumentAsync(CreateDocumentDto dto);

        /// <summary>
        /// Создание нескольких документов с базовыми параметрами
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<DocumentDto>> CreateDocumentsMultipleAsync(List<CreateDocumentDto> listModels);

        /// <summary>
        /// Обновление документа
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<DocumentDto>> UpdateDocumentAsync(DocumentDto model);

        /// <summary>
        /// Удаление документа по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task <BaseResult<DocumentDto>> DeleteDocumentAsync(long id);

        /// <summary>
        /// Получение отчета по пользователю
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CollectionResult<ExpenseReportDto>> GetReportAsync(ReportArgument argument);
    }
}
