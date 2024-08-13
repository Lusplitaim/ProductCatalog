import { UserRole } from "./userRole";

export interface CreateUser {
    userName: string;
    email: string;
    password: string;
    userRoles: UserRole[];
}