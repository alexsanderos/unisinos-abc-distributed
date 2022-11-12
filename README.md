## Architecture

C4 Model

### Context Diagram

![context](https://www.plantuml.com/plantuml/png/RLBBRjim4BppAxOwAK1jzP1JJqaS8mtWDaPj8yX9eAIDJ8HQ2ijbaw3er_JG3ycFbPILs7deHEtEpCuiUU95iuJEga-6QXiQXAr8xxyM1UlxQMja6whWaMj7WYJJsdL5RpKbpiHBOqrLp8udorJQV5yKdVQ274lbB34UPDedTRKulRdQhClho_MtSZ6_cFyupu-K4YCMOVKOEHsC16YCRbbtSEiOBib7J31F_vx-EZWzcocrHFQEyZfuSHtoPyXM4dsJPFiZM4Dts80C730x5J6EfbaSEu-3k6VZiJnpWHhDZtdz9TBVZSkEuBTI4B-ToVmUgz8Fnu_W8sfmiWl5w7j7tbGnPmhKi_jb6aCjtAAM4Dl9q1-bTlXo_Y1vfutT3raXDHTi6l0lskijGjmKCcaBE4duiGjzl4v27c8UI8xbij9qjvTUwdQ5RM2N3Jy3P72PO22ioUiwRMqm-HFjmTPVBMBZyGLosFXux8X-2tuT-rdo9CQFB-W_ "context")

### Container Diagram

![container](https://www.plantuml.com/plantuml/png/bLLDSzem4BtpArHweJC9RcOwFVIKy35DqeGmc4oEdhMz1Wsov97a4Dhfh-cXFoH_h2iPCE3G99n0gttlxTFkScuiQ5lcelM1oqIKARAvjONv5WGQbjqPj_CoBWtgH4cBqdOJbGU50EboJerD1O-3_fVJiJkwlxq9SZ0MDHr5VSe0Bb5t2uVV2Y_l7gBnS1BUZQAx--dDrMZO-THgZL4R9Jj9QQpAKP-mTcX9XhJj-YiJN2umPLQnYrwVSMaijwNbIdRR116kY3EFUggKAUXL9_bCgLVL9Vxyz_bFbLPTQtzYlrejHf-jm6ZuP3jBZ2Ce2ifym9XT58NZluP7C8dcXJrX5t9M2j3ixCn5-e8xOHfDeQJXiK2MASqAhHvvokMCsJcofBe4IOB6a6no9eC4dUOz0PsSuZ33YAmcxDlg5-kLnb4D7nt8PHbJx0Ta2t27KmsIgBNrj9H7ECI1xgvf4bJLyMrrV5DX_oCR3QViQtiJ6b_zfoZZBMGPWYtfLfD7wMIEncgmIaV9vfYmxtO23Lwb5wWRj3NMra-L5mAVM2RKiaa-YFVfq_Y0bmt0GWm6lG1bScNOe5VrcI9sZqlFHEGvkEvwmKSlDIfWbPFHjVPnTV36odmdGEBhMjA8-U9Ya0jctREwY9AieCaapkCYjCgum0WaY9NXPaTD5Q6MhI9lbEM1VcxCTbFFrXn7DKXbUSOJxrODFDevVIEt1revRcyEFHdDktE0LstsUplcRbZNraJEiwXgIe8Umop4MQbLs__mLwhnEB3M8qvGx6oTUlplZMTquUrXFJvz4CBG8d8KbzFfE0okQQ7jR24dBmSkQbaXolIxvkbwUlpqah_uH7MIumnFtVtDRArBwwubHfDwTlUtX8CrR5C1AuiKBAuNW1EupXySdSbzk74P2sxcGKXxXZPUZ3kxfC8vxFttvX_JqHpQ2GALbccLKq-cRAcvnTsQ9WqjBpU4Uvh0lv8p_HOMXE8Md7_43TZDoxryOFQhhZI6yNtsJ8wesEkaL-ivfvwWFzj_ "container")

## API Entrypoint events

<ul>
    <li>Technology: C#, .NET 6, Event Hubs (Kafka) and Masstransit</li>
    <li>Description: Web API responsible for providing system features</li>
</ul>

## Analyze Profile

<ul>
    <li>Technology: C#, .NET 6, Event Hubs (Kafka) and Masstransit</li>
    <li>Description: Worker responsible for interacting with the bank transaction partner</li>
</ul>

## Orchestrator

<ul>
    <li>Technology: C#, .NET 6, Event Hubs (Kafka), Masstransit and CosmosDB</li>
    <li>Description: Worker responsible for orchestrating the complex flows</li>
</ul>

![Orchestrator SAGA](./docs/sagas/diagram.svg)

## Notification

<ul>
    <li>Technology: C#, .NET 6, Event Hubs (Kafka) and Masstransit</li>
    <li>Description: Worker responsible for notifying interested parties</li>
</ul>

## Payment

<ul>
    <li>Technology: C#, .NET 6, Event Hubs (Kafka), Masstransit and CosmosDB</li>
    <li>Description: Worker responsible for interacting with the bank transaction partner</li>
</ul>
