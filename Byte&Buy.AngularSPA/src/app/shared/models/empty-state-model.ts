import { CtaButtonModel } from "./cta-button-model";

export interface EmptyStateModel{
    mainIconClass: string;
    header: string;
    description: string;
    buttonArray?: CtaButtonModel[];
    backgroundClass?: string;  
}