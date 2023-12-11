import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { KPI } from '../models/KPI';

@Injectable({
  providedIn: 'root'
})
export class KpiService {

  private url = environment.baseURL + 'api/kpis/';

  constructor(private http: HttpClient) { }

  getKPIsInDepartment(depNum: number): Observable<any> {
    var getURL: string = this.url + 'GetKPIsInDepartment/' + depNum.toString();
    return this.http.
      get<any>(getURL)
      .pipe();
  }

  deleteKPIsInDepartment(kpis: KPI[]): Observable<any> {
    return forkJoin(
      kpis.map((kpi) =>
        this.http.delete<KPI>(`${this.url}/${kpi.KPIIDNum}`)
      )
    );
  }

  saveKPIs(kpis: KPI[], depNum:number): Observable<any>[] {
    var res: Observable<any>[] = [];
    forkJoin(
      kpis.map((kpi) => { 
        if (kpi.KPIIDNum > 0) {
          res.push(this.http.put<KPI>(this.url + kpi.KPIIDNum.toString(), kpi));
        }
        else {
          kpi.KPIIDNum = 0;
          kpi.DepNo = depNum;
          res.push(this.http.post<KPI>(`${this.url}`, kpi));
        }
      })
    );

    //kpis.forEach((kpi, index) => {
      
    //  if (kpi.KPIIDNum > 0) {

    //    res.push(this.http.put<any>(this.url + kpi.KPIIDNum.toString(), kpi));
    //  }
    //  else {
    //    res.push(this.http.post<any>(`${this.url}`, kpi));
    //  }
    //});
    
    return res;
  }
}
