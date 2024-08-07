﻿namespace BaseCore.DAL.Implementations.Entities.Documents
{
    public class IncomingDocument : BaseDocument<IncomingDocumentEntry>
    {
        public long ShipperId { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public Contractor Shipper { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Total { get; set; }
    }



    public class IncomingDocumentEntry : BaseDocumentEntry
    {
        public IncomingDocument Document { get; set; }

        public decimal Price { get; set; }
    }
}
