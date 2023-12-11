import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
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
  total: string = '';
  measUnitOptions: string[] = ['Percentage', 'Number'];
  bInProgress: boolean = false;
  absDegreeMax: number = 500;
  constructor(private kpiService: KpiService,
    public snackBar: MatSnackBar) {
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
      this.clearAll();
      this.depNum = depNo;
      this.bInProgress = true;
      // fetch the city from the server
      this.kpiService.getKPIsInDepartment(this.depNum)
        .subscribe(result => {
          this.KPIs = new MatTableDataSource<KPI>(result.Data);
          this.bInProgress = false;
          this.calculateTotal();
        }, error => {
          this.KPIs.data = [];
          console.error(error);
          this.bInProgress = false;
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
    this.calculateTotal();
  }
  addNewKPIs(): void
  {
    this.clearAll();
  }

  DeleteKPIs(): void {
    this.bInProgress = true;
    this.kpiService.deleteKPIsInDepartment(this.KPIs.data)
      .subscribe(result => {
        this.clearAll();
        this.bInProgress = false;
        this.openSnackBar("All KPIs has been deleted successfully", "");
      }, error => {
        console.error(error);
        this.bInProgress = false;
        this.openSnackBar("An error occured while deleting", "");
      });
    
  }

  SaveKPIs(): void {
    if (this.calculateTotal() as unknown as Number <= 100) {
      this.bInProgress = true;
      this.kpiService.saveKPIs(this.KPIs.data, this.depNum).forEach((observableRes, index) => {
        observableRes.subscribe(
          result => {
            this.bInProgress = false;
            this.openSnackBar("All KPIs has been updated successfully", "");
          }, error => {
            console.error(error);
            this.bInProgress = false;
            this.openSnackBar("An error occured while saving", "");
          })
      });
    }
    else {
      this.openSnackBar("Couldn't save!!. the total targeted value percentage for the department cannot be more than 100%", "");
    }
  }

  // Gets the total targeted values in degrees
  calculateTotal(): string{
    var kpisInNumber: number[] = [];
    this.KPIs.data.forEach((kpi, i) => {
      if (kpi.MeasurementUnit==true)//percentage
        kpisInNumber.push(this.convertFromDegToPercentage(kpi.TargetedValue))
      else
        kpisInNumber.push(kpi.TargetedValue)
    });
    
    var totalNum = kpisInNumber
      .reduce((acc, value) => acc + value, 0);
    if (totalNum > 100) {
      this.openSnackBar("the total targeted value percentage for the department cannot be more than 100%", "");
    }
    this.total = totalNum.toString();

    return totalNum.toString();

  }

  private convertFromDegToPercentage(deg: number): number
  {
    var perc: number = deg * this.absDegreeMax / 100;
    return perc;

  }

  private convertFromPercentageToDeg(perc: number): number {
    var deg: number = perc * 100/ this.absDegreeMax;
    return deg;

  }

  toggleMeasUnit(kpi: KPI)
  {
    if (kpi.MeasurementUnit) {
      kpi.TargetedValue = this.convertFromPercentageToDeg(kpi.TargetedValue)
    }
    else {
      kpi.TargetedValue = this.convertFromDegToPercentage(kpi.TargetedValue)
    }
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }

}


