import { DeliveryGroup } from "../../../shared/models/delivery-group";
import { DeliveryOption } from "../../../shared/models/delivery-options";

export function groupByCarrier(options: DeliveryOption[]): DeliveryGroup[] {
    const map = new Map<string, DeliveryOption[]>();

    for (const option of options) {
        if (!map.has(option.carrier)) {
            map.set(option.carrier, []);
        }
        map.get(option.carrier)!.push(option);
    }

    return Array.from(map.entries()).map(([carrier, options]) => ({
        carrier,
        options
    }));

}