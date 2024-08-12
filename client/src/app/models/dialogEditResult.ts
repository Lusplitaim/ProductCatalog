export interface DialogEditResult<T> {
    result: T;
    action: DialogEditAction;
}

export type DialogEditAction = 'create' | 'update' | 'delete';