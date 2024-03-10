using WebApi.Domain.Dto.Document;
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
        Task<CollectionResult<DocumentDto>> GetDocumentsByExpenditureAsync(DateTime dateStart, DateTime dateEnd, short expenditureId);
        
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
    }
}
