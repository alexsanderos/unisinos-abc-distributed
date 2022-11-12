namespace Unisinos.Abc.Messages
{
    public static class Topics
    {
        public static class AnalyzeProfile
        {
            public static string AnalyzeProfileCreditCommand => "analyze-profile";
            public static string ApprovedProfileEvent => "approved-profile";
            public static string DisapprovedProfileEvent => "disapproved-profile";
        }

        public static class PaymentProcess
        {
            public static string PaymentProcessCommand => "payment-process";
            public static string PaymentCompletedEvent => "payment-completed";
            public static string PaymentErrorEvent => "payment-error";
        }

        public static class PaymentConfirm
        {
            public static string PaymentConfirmCommand => "payment-confirm";
            public static string PaymentConfirmedEvent => "payment-confirmed";
            public static string PaymentConfirmedErrorEvent => "payment-confirmed-error";
        }

        public static class PurchaseCreditCourse
        {
            public static string PurchaseCreditCourseCommand => "purchase-course";
        }
        public static class Notification
        {
            public static string NotificationCommand => "notification-process";
        }

        public static class DefaultGroup
        {
            public static string DefaultGroupId => "$Default";
        }
    }
}
