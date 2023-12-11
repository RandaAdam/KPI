import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from '../environments/environment';
import { KPI } from './models/KPI';
import { KpiService } from './services/kpi.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  public KPIs!: MatTableDataSource<KPI>;
  private newKPIsIndexs!: number[];
  public displayedColumns: string[] =
    ['kpiidnum', 'kpidescription', 'measurementunit', 'targetedvalue'];

  title = 'KPI';
  public depNum = 0;
  measUnitOptions: string[] = ['Percentage', 'Number'];
  constructor(private kpiService: KpiService) {
    this.newKPIsIndexs = [];
  }

  ngOnInit(): void {
    this.KPIs = new MatTableDataSource<KPI>();

    if (this.depNum > 0) {
      this.loadKPIsForDep(this.depNum);
    }
  }

  loadKPIsForDep(depNo: any): void {
    if (depNo > 0) {
      this.depNum = depNo;
      // fetch the city from the server
      this.kpiService.getKPIsInDepartment(this.depNum)
        .subscribe(result => {
          this.KPIs = new MatTableDataSource<KPI>(result.Data);
        }, error => {
          this.KPIs.data = [];
          console.error(error);
        });
    }
  }

  addNewRow(): void {

    const newRow: any = {
      KPIIDNum: '',
      KPIDescription: '',
      MeasurementUnit: true,
      TargetedValue: '',
      DepNo: ''
    }
    //this.newKPIsIndexs.push(this.KPIs.data.length);
    this.KPIs.data = [...this.KPIs.data, newRow] ?
      [...this.KPIs.data, newRow] : [newRow];


  }

  private clearAll()
  {
    this.KPIs.data = [];
    this.depNum = 0;
  }
  addNewKPIs(): void
  {
    this.clearAll();
  }

  DeleteKPIs(): void {
    this.kpiService.deleteKPIsInDepartment(this.KPIs.data)
      .subscribe(result => {
        this.clearAll();
      }, error => {
        console.error(error);
      });
    
  }

  SaveKPIs(): void {
    var rollBack: boolean = false;
    this.kpiService.saveKPIs(this.KPIs.data, this.depNum).forEach((observableRes, index) => {
      observableRes.subscribe(
        result => {

        }, error => {
          console.error(error);
          rollBack = true;
        })
    });
  }


}


