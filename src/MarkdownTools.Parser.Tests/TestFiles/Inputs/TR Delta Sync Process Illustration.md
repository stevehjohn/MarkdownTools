# TR Delta Sync

## Overview

This document is an overview of the database state during the synchronization process.

### Initial State

`Restaurants` table contains a snapshot of core at a particular point in time. Sequence number should be set to 0.  All other tables are empty.

> Restaurants

|SequenceNo|Id|...|
|---|---|---|
|0|456||
|0|945||
|0|674||
|0|125||

### Pre-Synchronization

During the period since initial setup, the SNS monitoring Lambdas will have been populating the `SynchronizationQueue` table.

> SynchronizationQueue

|SequenceNo|RestaurantId|
|---|---|
|1|828|
|2|834|
|3|383|
|4|674|

### Syncronization Start

Service selects `MAX(EndSequenceNo)` from `SynchronizationHistory` which will be 0 at this point. Service then selects all `RestaurantId`s from `SynchronizationQueue` where `SequenceNo` > 0. Calls API for each of those restaurants to get the latest state of the data and populates `Restaurants` accordingly.

Service updates D365 with restaturants who's `SequenceNo` > 0 AND <= 4. Then logs to `SynchronizationHistory`.

> Restaurants

|SequenceNo|Id|...|
|---|---|---|
|0|456||
|0|945||
|0|674||
|0|125||
|1|828||
|2|834||
|3|383||
|4|674||


> SynchronizationHistory

|Id|StartSequenceNo|EndSequenceNo|
|---|---|---|
|1|1|4|

### After Syncronization

`SynchronizationQueue` is continually populated by messages from SNS.

> SynchronizationQueue

|SequenceNo|RestaurantId|
|---|---|
|1|828|
|2|834|
|3|383|
|4|674|
|5|896|
|6|235|
|7|856|
|8|591|
|9|859|

### Next Synchronization

Service selects `MAX(EndSequenceNo)` from `SynchronizationHistory`, which is 4. Service then selects all `RestaurantId`s from `SynchronizationQueue` where `SequenceNo` > 4. Calls API for each of those restaurants to get the latest state of the data and populates `Restaurants` accordingly.

Service updates D365 with restaturants who's `SequenceNo` > 4 AND <= 9. Then logs to `SynchronizationHistory`.

> Restaurants

|SequenceNo|Id|...|
|---|---|---|
|0|456||
|0|945||
|0|674||
|0|125||
|1|828||
|2|834||
|3|383||
|4|674||
|5|896||
|6|235||
|7|856||
|8|591||
|9|859||

> SynchronizationHistory

|Id|StartSequenceNo|EndSequenceNo|
|---|---|---|
|1|1|4|
|2|5|9|

### And so on...