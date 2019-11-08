<template>
    <el-dialog
        title="Позиция документа"
        :visible.sync="dialogVisible"
        :before-close="onCancel"
        draggable
        modal
        append-to-body
        width="40%">

        <el-form v-model="entry">
            <el-form-item label="Продукт">
                <el-input placeholder="Выбирете продукт" readonly v-model="productTitle">
                    <el-button slot="append" icon="el-icon-search" @click="searchProduct"/>
                </el-input>
            </el-form-item>

            <el-form-item label="Количество" v-model="entry.count">
                <el-input-number v-model="entry.count"></el-input-number>
            </el-form-item>
        </el-form>

        <SelectProduct :dialogVisible.sync="selectProductVisible" v-on:onProductSelect="onProductSelect"/>

        <footer slot="footer" class="dialog-footer">
            <el-button type="primary" @click="onSubmit">Выбрать</el-button>
            <el-button @click="onCancel">Отмена</el-button>
        </footer>

    </el-dialog>
</template>

<script>
import SelectProduct from '@/components/SelectProduct/index'

export default {
    name: 'DocumentEntryEdit',
    components: { SelectProduct },
    props: {
        entry: {
            type: Object,
            required: true
        },
        dialogVisible: {
            type: Boolean,
            required: true,
            default: false
        }
    },
    data() {
        return {
            productTitle: null,
            selectProductVisible: false
        }
    },
    methods: {
        onSubmit() {
            if (this.entry == null) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Необходимо выбрать продукт'
                })
                return
            }

            if (this.entry.product == null) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Необходимо выбрать продукт'
                })
                return
            }

            if (this.entry.count <= 0) {
                this.$notify.error({
                    title: 'Ошибка',
                    message: 'Количество должно быть больше 0'
                })
                return
            }

            this.$emit('onSubmit', this.entry)
            
        },
        searchProduct() {
            this.selectProductVisible = true
        },
        onCancel() {
            this.$emit('update:dialogVisible', false)
        },
        onProductSelect(val) {
            this.entry.productId = val.id
            this.entry.product = val
            this.productTitle = val.title
            this.selectProductVisible = false
        }
    }
}
</script>
