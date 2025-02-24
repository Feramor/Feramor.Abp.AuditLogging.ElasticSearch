import { InjectionToken } from '@angular/core';

export const SHOW_ENTITY_HISTORY = new InjectionToken<(entityId: string, entityTypeFullName: string) => void>(
  'SHOW_ENTITY_HISTORY',
);
