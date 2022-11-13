namespace Unisinos.Abc.Messages
{
    public static class Topics
    {
        public static class AnalyzeProfile
        {
            public static string AnalyzeProfileCreditCommand => "analyze-profile";
            public static string AnalyzeProfileCreditCommandGroupId => $"{AnalyzeProfileCreditCommand}-{DefaultGroup.DefaultGroupId}";
            public static string ApprovedProfileEvent => "approved-profile";
            public static string ApprovedProfileEventGroupId => $"{ApprovedProfileEvent}-{DefaultGroup.DefaultGroupId}";
            public static string DisapprovedProfileEvent => "disapproved-profile";
            public static string DisapprovedProfileEventGroupId => $"{DisapprovedProfileEvent}-{DefaultGroup.DefaultGroupId}";
        }

        public static class PaymentProcess
        {
            public static string PaymentProcessCommand => "payment-process";
            public static string PaymentProcessCommandGroupId => $"{PaymentProcessCommand}-{DefaultGroup.DefaultGroupId}";
            public static string PaymentCompletedEvent => "payment-completed";
            public static string PaymentCompletedEventGroupId => $"{PaymentCompletedEvent}-{DefaultGroup.DefaultGroupId}";
            public static string PaymentErrorEvent => "payment-error";
            public static string PaymentErrorEventGroupId => $"{PaymentErrorEvent}-{DefaultGroup.DefaultGroupId}";
        }

        public static class PaymentConfirm
        {
            public static string PaymentConfirmCommand => "payment-confirm";
            public static string PaymentConfirmCommandGroupId => $"{PaymentConfirmCommand}-{DefaultGroup.DefaultGroupId}";
            public static string PaymentConfirmedEvent => "payment-confirmed";
            public static string PaymentConfirmedEventGroupId => $"{PaymentConfirmedEvent}-{DefaultGroup.DefaultGroupId}";
            public static string PaymentConfirmedErrorEvent => "payment-confirmed-error";
            public static string PaymentConfirmedErrorEventGroupId => $"{PaymentConfirmedErrorEvent}-{DefaultGroup.DefaultGroupId}";
        }

        public static class PurchaseCreditCourse
        {
            public static string PurchaseCreditCourseCommand => "purchase-course";
            public static string PurchaseCreditCourseCommandGroupId => $"{PurchaseCreditCourseCommand}-{DefaultGroup.DefaultGroupId}";
        }
        public static class Notification
        {
            public static string NotificationCommand => "notification-process";
            public static string NotificationCommandGroupId => $"{NotificationCommand}-{DefaultGroup.DefaultGroupId}";
        }

        public static class DefaultGroup
        {
            public static string DefaultGroupId => "group";
        }
    }
}
