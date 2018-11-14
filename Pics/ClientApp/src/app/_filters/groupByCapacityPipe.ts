import { Pipe, PipeTransform } from '@angular/core'

@Pipe({
    name: 'groupByCapacity',
    pure: false
})
export class GroupByCapacity implements PipeTransform {
    transform(items: any[], filter: number): any[] {
        if (!items || !filter) {
            return items;
        }
        let result: any[] = [];
        for (let i = 0, j = 0, groupName = 0; i < items.length; j++ , i++) {
            if (j === 0)
                result[groupName] = [];

            result[groupName].push(items[i]);

            if (j === filter - 1) {
                j = -1;
                groupName++;
            }
        }

        return result;
    }
}