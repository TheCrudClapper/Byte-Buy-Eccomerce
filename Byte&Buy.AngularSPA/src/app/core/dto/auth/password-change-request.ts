export interface PasswordChangeRequest{
    newPassword: string;
    currentPassword: string;
    confirmPassword: string;
}