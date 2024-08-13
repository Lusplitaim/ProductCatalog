import { UserRole } from "./userRole";

export interface EditUser {
    userName: string;
    updatedPassword?: string;
    addedRoles: UserRole[];
    removedRoles: UserRole[];
}