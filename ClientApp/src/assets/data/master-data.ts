export const MasterData = {

  itemcategory:{
    mainEquipment:1,
    spares:2,
    consumble:3,
    returnable:5,
    lifeLimitItem:6,
    Misc:7
  },
  equipmentType:{
    Major: 1,
    Minor:2
  },
  sparescategory:{
   spares:1,
   tools:2
  },
      paging: {
        showFirstLastButtons: true,
        pageIndex: 1,
        pageSize: 10,
        pageSizeOptions: [5, 10, 15, 20, 25, 50, 100, 200, 500, 1000]
      },

      codevaluetype: {
        LocationType: "LocationType",
        ResultStatus:"ResultStatus",
        DeadStatus:"DeadStatus",
        FundingDetail:"FundingDetail",
        ModuleIcon:"Material Icon"
      },
      country: {
        bangladesh:1
      },
      department: {
        all:0,
        mpa:1,
        hel:2,
        uav:4
      },
      schoolDept:{
        navalAviation: 3,
      },

      baseSchoolDept:{
        submarine: 13,
      },


      procurement:{
        itemStatusProcurement: 6,
        itemStatusSFT: 7,
        itemStatusStore: 8,
        procurementStatus: 0,
      },

      demandCompleteStatus:{
        completed:1,
        notComplete:0
      },

      itemStatus:{
        itemStatusProcurement: 6,
        itemStatusSFT: 7,
        itemStatusStore: 8,
      },
      status:{
        demandStatus:0,
        procurementStatus:0,
        storeStatus:0
    }
    }