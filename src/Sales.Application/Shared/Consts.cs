namespace Sales.Application.Shared
{
    public static class Consts
    {
        #region Results Messages

        public const string EntityCreatedWithSuccess = "{0} created with success";
        public const string GetEntityByIdWithSuccess = "Getting {0} by id with success";
        public const string GetEntitiesWithSuccess = "Getting {0}s with success";
        public const string NotFoundEntity = "{0} not found";
        public const string NotFoundEntityById = "{0} with Id {1} was not found";
        public const string SaleCanceledWithSuccess = "Sale with Id {0} was canceled with success";

        #endregion Results Messages

        #region Validations Messages

        public const string FieldContainInvalidValue = "Field(s) {0} contain(s) invalid value(s)";
        public const string FieldCannotBeNullOrEmpty = "Field {0} cannot be null or empty";
        public const string FieldMustBeGreaterThan = "Field {0} must be greater than {1}";
        public const string FieldMustBeLowerOrEqualTo = "Field {0} must be lower or equal to {1}";
        public const string DuplicatedProductIds = "The following ProductId(s) is/are duplicated: {0}";
        public const string GuidCannotBeEmptyGuid = "Field {0} cannot be empty guid";

        #endregion Validations Messages
    }
}