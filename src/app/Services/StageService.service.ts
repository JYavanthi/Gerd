// stage.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StageService {
  private actualStageSource = new BehaviorSubject<number | null>(null);
  actualStage$ = this.actualStageSource.asObservable();

  setActualStage(stage: number) {
    this.actualStageSource.next(stage);
  }

  getActualStage(): number | null {
    return this.actualStageSource.getValue();
  }
}
