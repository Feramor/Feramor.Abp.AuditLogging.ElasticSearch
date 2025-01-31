import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';
import { collapse, ToasterService } from '@abp/ng.theme.shared';
import { finalize, map, Observable } from 'rxjs';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ElasticSearchAuditLogSettingsDto, ElasticSearchSettingsService } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';
import { ElasticSearchAuthenticationType, elasticSearchAuthenticationTypeOptions } from '@feramor/ng.abp-audit-logging-elastic-search/proxy';

@Component({
  selector: 'lib-elastic-search-settings',
  templateUrl: './elastic-search-settings.component.html',
  styleUrl: './elastic-search-settings.component.css',
  animations: [collapse]
})
export class ElasticSearchSettingsComponent implements OnInit  {
  protected readonly toasterService: ToasterService = inject(ToasterService);
  protected readonly cdr: ChangeDetectorRef = inject(ChangeDetectorRef);
  protected readonly configState: ConfigStateService = inject(ConfigStateService);
  protected readonly fb = inject(UntypedFormBuilder);

  private _loading: boolean;
  set loading(value: boolean) {
    this._loading = value;
    this.cdr.markForCheck();
  }

  get loading() {
    return this._loading;
  }

  settings$: Observable<ElasticSearchAuditLogSettingsDto>;
  form: UntypedFormGroup;

  elasticSearchAuthenticationTypes = elasticSearchAuthenticationTypeOptions;

  constructor(
    private configStateService: ConfigStateService,
    private service: ElasticSearchSettingsService
  ) {}

  ngOnInit() {
    this.settings$ = this.service.get().pipe(map(response => {
      this.buildForm(response);
      return response;
    }));
  }

  protected buildForm(settings: ElasticSearchAuditLogSettingsDto) {
    this.form = this.fb.group({
      isActive: [settings.isActive, [Validators.required]],
      uri: [settings.uri, [Validators.required]],
      useSsl: [settings.useSsl, []],
      sslFingerprint: [settings.sslFingerprint, []],
      index: [settings.index, [Validators.required]],
      authenticationType: [settings.authenticationType, []],
      username: [settings.username, []],
      changePassword: [false, []],
      password: [undefined, []],
      changeApiKey: [false, []],
      apiKey: [undefined, []],
      changeApiKeyId: [false, []],
      apiKeyId: [undefined, []],
      isPeriodicDeleterEnabled: [settings.isPeriodicDeleterEnabled, [Validators.required]],
      periodicDeleterCron: [settings.periodicDeleterCron, [Validators.required]],
      periodicDeleterPeriod: [settings.periodicDeleterPeriod, [Validators.required, Validators.min(1)]],
    });

    this.useSslChanged(settings.useSsl);
    this.isActiveChanged(settings.isActive);
    this.authenticationTypeChanged(settings.authenticationType);

    this.form.get("useSsl").valueChanges.subscribe(value => {
      this.useSslChanged(value);
    });

    this.form.get("isActive").valueChanges.subscribe(value => {
      this.isActiveChanged(value);
    });

    this.form.get("changePassword").valueChanges.subscribe(value => {
      this.changePasswordChanged(value);
    });

    this.form.get("changeApiKeyId").valueChanges.subscribe(value => {
      this.changeApiKeyIdChanged(value);
    });

    this.form.get("changeApiKey").valueChanges.subscribe(value => {
      this.changeApiKeyChanged(value);
    });

    this.form.get("authenticationType").valueChanges.subscribe(value => {
      this.authenticationTypeChanged(value);
    });

    this.cdr.detectChanges();
  }

  useSslChanged(value) {
    if (value) {
      this.form.get("sslFingerprint").addValidators([Validators.required]);
      this.form.get("sslFingerprint").updateValueAndValidity();
    } else {
      this.form.get("sslFingerprint").removeValidators([Validators.required]);
      this.form.get("sslFingerprint").updateValueAndValidity();
    }
  }

  changePasswordChanged(value) {
    if (value) {
      this.form.get("password").addValidators([Validators.required]);
      this.form.get("password").updateValueAndValidity();
    } else {
      this.form.get("password").removeValidators([Validators.required]);
      this.form.get("password").setValue(undefined);
      this.form.get("password").updateValueAndValidity();
    }
  }

  changeApiKeyIdChanged(value) {
    if (value) {
      this.form.get("apiKeyId").addValidators([Validators.required]);
      this.form.get("apiKeyId").updateValueAndValidity();
    } else {
      this.form.get("apiKeyId").removeValidators([Validators.required]);
      this.form.get("apiKeyId").setValue(undefined);
      this.form.get("apiKeyId").updateValueAndValidity();
    }
  }

  changeApiKeyChanged(value) {
    if (value) {
      this.form.get("apiKey").addValidators([Validators.required]);
      this.form.get("apiKey").updateValueAndValidity();
    } else {
      this.form.get("apiKey").removeValidators([Validators.required]);
      this.form.get("apiKey").setValue(undefined);
      this.form.get("apiKey").updateValueAndValidity();
    }
  }

  isActiveChanged(value) {
    if (!value) {
      if (this.form.get("uri").invalid) {
        this.form.get("uri").setValue("http://localhost:9200");
        this.form.get("uri").updateValueAndValidity();
      }
      if (this.form.get("index").invalid) {
        this.form.get("index").setValue("feramor-abp-audit-logging");
        this.form.get("index").updateValueAndValidity();
      }
      if (this.form.get("sslFingerprint").invalid) {
        this.form.get("useSsl").setValue(false);
        this.form.get("useSsl").updateValueAndValidity();
      }
    }
  }

  authenticationTypeChanged(value: ElasticSearchAuthenticationType) {
    if (value !== undefined && value !== null) {
      if (value == ElasticSearchAuthenticationType.BasicAuthentication) {
        this.form.get("apiKey").removeValidators([Validators.required]);
        this.form.get("apiKey").updateValueAndValidity();

        this.form.get("apiKeyId").removeValidators([Validators.required]);
        this.form.get("apiKeyId").updateValueAndValidity();

        this.form.get("username").addValidators([Validators.required]);
        this.form.get("username").updateValueAndValidity();
      } else if (value == ElasticSearchAuthenticationType.ApiKeyAuthentication) {
        this.form.get("username").removeValidators([Validators.required]);
        this.form.get("username").updateValueAndValidity();

        this.form.get("password").removeValidators([Validators.required]);
        this.form.get("password").updateValueAndValidity();

        this.form.get("apiKeyId").removeValidators([Validators.required]);
        this.form.get("apiKeyId").updateValueAndValidity();
      } else {
        this.form.get("username").removeValidators([Validators.required]);
        this.form.get("username").updateValueAndValidity();

        this.form.get("password").removeValidators([Validators.required]);
        this.form.get("password").updateValueAndValidity();
      }
    } else {
      this.form.get("username").removeValidators([Validators.required]);
      this.form.get("username").updateValueAndValidity();

      this.form.get("password").removeValidators([Validators.required]);
      this.form.get("password").updateValueAndValidity();

      this.form.get("apiKey").removeValidators([Validators.required]);
      this.form.get("apiKey").updateValueAndValidity();

      this.form.get("apiKeyId").removeValidators([Validators.required]);
      this.form.get("apiKeyId").updateValueAndValidity();
    }
  }

  submit() {
    if (this.form.invalid) {
      return;
    }
    this.loading = true;
    this.service
      .update(this.form.value)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(() => {
        this.toasterService.success('AbpUi::SavedSuccessfully', null);
      });
  }

  testConnection() {
    this.loading = true;
    this.service
      .testConnection()
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(() => {
        this.toasterService.success('ElasticSearch::ConnectionSuccessful', null);
      });
  }

  protected readonly ElasticSearchAuthenticationType = ElasticSearchAuthenticationType;
}
