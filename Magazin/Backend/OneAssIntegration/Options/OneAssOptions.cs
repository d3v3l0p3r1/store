namespace OneAssIntegration.Options
{
    /// <summary>
    /// Настройки импорта из 1С
    /// </summary>
    public class OneAssOptions
    {
        /// <summary>
        /// Наименование склада
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        /// Строка, код узла обмена, в котором указаны настройки обмена
        /// </summary>
        public string SettingsExchangeCode { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
