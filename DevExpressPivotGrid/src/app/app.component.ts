import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import PivotGridDataSource from 'devextreme/ui/pivot_grid/data_source';
import CustomStore from 'devextreme/data/custom_store';

@Component({
  styleUrls: ['./app.component.css'],
  selector: 'app-demo-app',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  dataSource: PivotGridDataSource;

  constructor(private httpClient: HttpClient) {
  }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    const store = new CustomStore({
      load: (loadOptions: unknown) => {
        return this.httpClient
          .post<any>('https://localhost:5001/Person', { options: loadOptions })
          .toPromise()
          .then((data: any) => {
            if (data.summary) {
              return {
                data: data.data,
                summary: data.summary
              };
            }
            else { return data.data; }
          })
          .catch(error => {
            throw new Error('Data Loading Error');
          });
      }
    });

    const fields = [
      {
        dataField: 'id',
        width: 25,
        area: 'row' as const
      },
      {
        caption: 'LastName',
        dataField: 'lastName',
        area: 'column' as const
      },
      {
        caption: 'FirstName',
        dataField: 'firstName',
        summaryType: 'min',
        area: 'data' as const
      },
      {
        dataField: 'birthTime',
        caption: 'BirthTime',
        dataType: 'date' as const,
        area: 'data' as const,
        summaryType: 'min',
        format: 'yyyy/MM/dd'
      },
    ];
    this.dataSource = new PivotGridDataSource({
      remoteOperations: false,
      store,
      fields,
    });
  }
}
