Namespace GOSHRM.GOSHRM.BO
    Public Class clsTrainSession
#Region "Private Variables"
        Private _id As String
        Private _name As String
        Private _course As String
        Private _detail As String
        Private _scheduledate As Date
        Private _scheduletime As String
        Private _duedate As Date
        Private _deliverymethod As String
        Private _deliverylocation As String
        Private _attendancetype As String
        Private _attachment As Byte
        Private _status As String
        Private _contenttype As String
        Private _attachname As String
        Private _HRApproval As String
        Private _currency As String
        Private _cost As Double
        Private _coordinator As String
        Private _trainingtype As String
        Private _HODApprove As Boolean
        Private _CoachApprove As Boolean
        Private _RequiresHR As String
        Private _applicationassessdate As Date
#End Region

#Region "Public Properties"
        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
            End Set
        End Property
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Course() As String
            Get
                Return _course
            End Get
            Set(ByVal value As String)
                _course = value
            End Set
        End Property
        Public Property Coordinator() As String
            Get
                Return _coordinator
            End Get
            Set(ByVal value As String)
                _coordinator = value
            End Set
        End Property
        Public Property TrainingType() As String
            Get
                Return _trainingtype
            End Get
            Set(ByVal value As String)
                _trainingtype = value
            End Set
        End Property
        'Public Property Detail() As String
        '    Get
        '        Return _detail
        '    End Get
        '    Set(ByVal value As String)
        '        _detail = value
        '    End Set
        'End Property
        Public Property ScheduleDate() As Date
            Get
                Return _scheduledate
            End Get
            Set(ByVal value As Date)
                _scheduledate = value
            End Set
        End Property
        Public Property ScheduleTime() As String
            Get
                Return _scheduletime
            End Get
            Set(ByVal value As String)
                _scheduletime = value
            End Set
        End Property
        'TimeSheet.StartTime = radStartTime.SelectedDate.Value.TimeOfDay.ToString
        Public Property DueDate() As Date
            Get
                Return _duedate
            End Get
            Set(ByVal value As Date)
                _duedate = value
            End Set
        End Property
        Public Property DeliveryMethod() As String
            Get
                Return _deliverymethod
            End Get
            Set(ByVal value As String)
                _deliverymethod = value
            End Set
        End Property
        Public Property DeliveryLocation() As String
            Get
                Return _deliverylocation
            End Get
            Set(ByVal value As String)
                _deliverylocation = value
            End Set
        End Property
        'Public Property AttendanceType() As String
        '    Get
        '        Return _attendancetype
        '    End Get
        '    Set(ByVal value As String)
        '        _attendancetype = value
        '    End Set
        'End Property
        'Public Property Attachement() As Byte
        '    Get
        '        Return _attachment
        '    End Get
        '    Set(ByVal value As Byte)
        '        _attachment = value
        '    End Set
        'End Property
        'Public Property CertificateRequired() As String
        '    Get
        '        Return _certificaterequired
        '    End Get
        '    Set(ByVal value As String)
        '        _certificaterequired = value
        '    End Set
        'End Property
        'Public Property Status() As String
        '    Get
        '        Return _status
        '    End Get
        '    Set(ByVal value As String)
        '        _status = value
        '    End Set
        'End Property
        'Public Property ContentType() As String
        '    Get
        '        Return _contenttype
        '    End Get
        '    Set(ByVal value As String)
        '        _contenttype = value
        '    End Set
        'End Property
        'Public Property AttachName() As String
        '    Get
        '        Return _attachname
        '    End Get
        '    Set(ByVal value As String)
        '        _attachname = value
        '    End Set
        'End Property
        'Public Property HRToAprove() As String
        '    Get
        '        Return _HRApproval
        '    End Get
        '    Set(ByVal value As String)
        '        _HRApproval = value
        '    End Set
        'End Property
        Public Property Currency() As String
            Get
                Return _currency
            End Get
            Set(ByVal value As String)
                _currency = value
            End Set
        End Property
        Public Property Cost() As Double
            Get
                Return _cost
            End Get
            Set(ByVal value As Double)
                _cost = value
            End Set
        End Property
        'Public Property HODsToApprove() As Boolean
        '    Get
        '        Return _HODApprove
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _HODApprove = value
        '    End Set
        'End Property
        'Public Property CoachesToApprove() As Boolean
        '    Get
        '        Return _CoachApprove
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _CoachApprove = value
        '    End Set
        'End Property
        'Public Property HRApprovalRequired() As String
        '    Get
        '        Return _RequiresHR
        '    End Get
        '    Set(ByVal value As String)
        '        _RequiresHR = value
        '    End Set
        'End Property
        Public Property ApplicationAssessmentDate() As Date
            Get
                Return _applicationassessdate
            End Get
            Set(ByVal value As Date)
                _applicationassessdate = value
            End Set
        End Property
#End Region
    End Class
End Namespace
